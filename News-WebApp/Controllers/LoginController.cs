using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News_WebApp.Models;
using News_WebApp.Repository;
namespace News_WebApp.Controllers
{
    public class LoginController : Controller
    {
        /* 
         * Retrieve the UserRepository object from the dependency Container through constructor Injection.
        */

        /*Call The Index Action to display default view
         
        */
       
        /*Use a Httpverb i.e POST for action i.e Index which acccepts User Object as a parameter
         * check the user credentials true or false. 
         * If user credentials are true store the userid in temporary storage and redirect the action to News controller and default Index view
         * If user creadentials are false display an error message(AddModelError) i.e Invalid User Id and Password
         */
        
    }
}