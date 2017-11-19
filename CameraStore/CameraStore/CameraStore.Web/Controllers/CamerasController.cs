

namespace CameraStore.Web.Controllers
{
    using Models.Cameras;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using CameraStore.Service;
    using Microsoft.AspNetCore.Identity;
    using CameraStore.Data.Models;

    public class CamerasController : Controller
    {
        private readonly UserManager<User> user;
        private readonly ICameraService cameras;

        public CamerasController(ICameraService cameras,
            UserManager<User> userManager)
        {
            this.user = userManager;
            this.cameras = cameras;
        }


        [Authorize]
        public IActionResult Add() => this.View();

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddCameraViewModel cameraModel)
        {
            if (!ModelState.IsValid)
            {
                return View(cameraModel);
            }

            this.cameras.Create(
                cameraModel.Make,
                cameraModel.Model,
                cameraModel.Price,
                cameraModel.Quantity,
                cameraModel.MinShutterSpeed,
                cameraModel.MaxShutterSpeed,
                cameraModel.MinISO,
                cameraModel.MaxISO,
                cameraModel.IsFullFrame,
                cameraModel.Description,
                cameraModel.ImageURL,
                cameraModel.VideoResolution,
                this.user.GetUserId(User),
                cameraModel.LightMetering
                          
                );


            return Redirect("/home");
        }
    }
}
