using CameraStore.Data.Models;
using System.Collections.Generic;

namespace CameraStore.Service
{
    public interface ICameraService
    {
        void Create(
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
             );


    }
}
