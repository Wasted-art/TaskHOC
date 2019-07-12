using System.Web.Mvc;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;


namespace MemberAPI_LiveDemo.Controllers
{
    public class RegisterController : SurfaceController
    {
        public ActionResult Register(RegisterModel model)
        {
            if (!ModelState.IsInvalid)
                return CurrentUmbracoPage();
            //Member Creation Code
            var memberService = Services.Memberservice;

            //check that member is uniqe
            if (memberService.GetByEmail(model.Email) != null)
            {
                ModelState.AddModelError("", "A member with that email allready exists");
                //Going back to main page
                return CurrentUmbracoPage();
            }

            var member = memberService.CreateMember(model.Email, model.Email, model.Name, "newMembers");

            memberService.SavePassword(member, model.Password);
            memberService.Login(model.Email, model.Password);

            return Redirect("/");
        }
    }

}