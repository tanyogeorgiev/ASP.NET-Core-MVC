using System;
using System.Collections.Generic;
using System.Text;

namespace CameraStore.Service.Implementations
{
    using CameraStore.Data;
    using Data.Models;
    using System.Linq;

    public class CameraService : ICameraService
    {
        private readonly CameraStoreDbContext db;

        public CameraService(CameraStoreDbContext db)
        {
            this.db = db;
        }

        public void Create(
            CameraMakeType make,
            string model,
            decimal price,
            int quantity,
            int minShutterSpeed,
            int maxShutterSpeed,
            MinISO minISO,
            int maxISO,
            bool isFoolFrame,
            string description,
            string imageURL,
            string videoResolution,
            string userId,
            IEnumerable<LightMetering> lightMetering
            )
        {

            var camera = new Camera
            {
                Make = make,
                Model = model,
                Price = price,
                Quantity = quantity,
                MinShutterSpeed = minShutterSpeed,
                MaxShutterSpeed = maxShutterSpeed,
                MinISO = minISO,
                MaxISO = maxISO,
                IsFullFrame = isFoolFrame,
                Description = description,
                ImageURL = imageURL,
                VideoResolution = videoResolution,
                UserId = userId,
                LightMetering = (LightMetering)lightMetering.Cast<int>().Sum()
            };

            this.db.Add(camera);
            this.db.SaveChanges();
        }
    }
}
