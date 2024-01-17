1.Clone Repository
2.create ‘Models’ folder, new item in that folder, create variables based on task
3.Install Microsoft.EntityFramewrkcore.Tools version 6.0.25 from NugetPackageManager 
4.add Dbset in ApplicationDbContext.cs file code, before constructor :   
public DbSet<FirstModel> FirstModels { get; set; } 
5.Add Migration:  open Nuget console run:         1: Add-Migration InitialCreate
						2: Update-Database 
  	Then check you must have database.db file in project.
6.Add controller: Right click on controller folder -> add -> controller -> api -> api controller with actions using entity framework (third in list).  Then choose model (created by you) and context file.
Test: run the program (start without debugging) try out any request to make sure you get 200 or 201 response code, so everything works fine. 
7. Since visual studio generated endpoints, we might need to add some endpoints yourself.
Copy-paste one of the codes, logically the most similar. For example if you want add get endpoint which selects information by name you mut copy paste getbyId endpoint, change properties, return type and function name as needed then rebuild the project, test endpoint in swagger, if you get 500 error browse error message in  browser, if you get response status 200 or 201 and it returns values logically correct you’re good to go. 
Get endpoint for 
	[HttpGet("find/name/{name}")]
public async Task<ActionResult<IEnumerable<FirstModel>>> GetByName(string name)
{
    if (_context.FirstModels == null)
    {
        return NotFound();
    }
    var firstModel = await _context.FirstModels.Where(m => m.Name == name).ToListAsync();

    if (firstModel == null)
    {
        return NotFound();
    }

    return firstModel;
}

[HttpGet("find/email/{email}")]
public async Task<ActionResult<IEnumerable<FirstModel>>> GetByEmail(string email)
{
    if (_context.FirstModels == null)
    {
        return NotFound();
    }
    var firstModel = await _context.FirstModels.Where(m => m.Email == email).ToListAsync();

    if (firstModel == null)
    {
        return NotFound();
    }

    return firstModel;
}

8.Custom Validation Attribute:
	1. create Validations folder, and add items (in our case Email and Phone),  both classes must inherit : ValidationAttribute. Then override IsValid method isValid method has two properties value and validationContext, we work on value, consider caes when will be invalid code we return:  return new ValidationResult("");when value is valid we return: 
 return ValidationResult.Success; as it seen on example

protected override ValidationResult IsValid(object value, ValidationContext validationContext)
{
    if (value == null)
    {
        return new ValidationResult("Email address is required.");
    }

    if (value is string email)
    {
        if (email.Contains('@') && email.Contains('.'))
        {
            return ValidationResult.Success;
        }
        else
        {
            return new ValidationResult("Invalid email format.");
        }
    }

    return new ValidationResult("The email address must be a string.");
}
2.	move to model class include  validation folder as it is on example: 
using FinalsProject.Validations;                                                                                                Then  above the variable we want to validate write [validationFileName] as it is on example: 
           [EmailAddress]
  public string Email { get; set; }

3.	To finalize everything we need to insert code in controller, in the endpoint method where we want to check value’s validity, in this case post endpoint where user inserts values. Example: 


In Phone.cs
[HttpPost]
public async Task<ActionResult<FirstModel>> PostFirstModel(FirstModel firstModel)
{
if (!ModelState.IsValid)
{
    return BadRequest(ModelState);
}
.
.
.
//controller code

}

   That’s all we do same for phone validation, code: 
using System.ComponentModel.DataAnnotations;

namespace FinalsProject.Validations
{
    public class Phone : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Phone number is required");
            }
            if (value is string phone)
            {
                long number;
                if (long.TryParse(phone, out number))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Invalid");
                }
            }
            else
            {
                return new ValidationResult("Invalid");
            }
        }
    }
}
In model:
        [Phone]
        public string PhoneNumber { get; set; }
In controller: we don’t need to write anything there because what we wrote in it checks validity of whole request: 


if (!ModelState.IsValid)
{
    return BadRequest(ModelState);
}
.

