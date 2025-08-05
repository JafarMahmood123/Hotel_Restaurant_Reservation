using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.AddAmenityToHotel;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.AddHotel;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.AssignPropertyTypeToHotel;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.DeleteHotel;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.RemoveAmenityFromHotel;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.UpdateHotel;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.UpdateHotelAmenity;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries.GetAllHotels;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries.GetAmenitiesByHotelId;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries.GetHotelById;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries.GetRoomsByHotelId;
using Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands.RemoveHotelImage;
using Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands.UploadHotelImage;
using Hotel_Restaurant_Reservation.Application.Implementation.Images.Queries.GetHotelImagesByHotelId;
using Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Commands.AddRoom;
using Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Commands.AddRoomToHotel;
using Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Commands.RemoveRoomFromHotel;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using Hotel_Restaurant_Reservation.Presentation.ApiModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers
{
    public class HotelsController : ApiController
    {

        public HotelsController(ISender sender) : base(sender)
        {
        }

        [HttpGet("{id:guid}")]
        [ActionName("GetHotelById")]
        public async Task<IActionResult> GetHotelById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetHotelByIdQuery(id);
            var result = await Sender.Send(query, cancellationToken);

            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpGet("{hotelId:guid}/rooms")]
        public async Task<IActionResult> GetRoomsByHotelId(Guid hotelId, CancellationToken cancellationToken)
        {
            var query = new GetRoomsByHotelIdQuery(hotelId);
            var result = await Sender.Send(query, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpGet("{hotelId:guid}/amenities")]
        public async Task<IActionResult> GetAmenitiesByHotelId(Guid hotelId, CancellationToken cancellationToken)
        {
            var query = new GetAmenitiesByHotelIdQuery(hotelId);
            var result = await Sender.Send(query, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{hotelId:guid}/amenities/{amenityId:guid}")]
        public async Task<IActionResult> AddAmenityToHotel(Guid hotelId, Guid amenityId,
            AddAmenityToHotelRequest request, CancellationToken cancellationToken)
        {
            var query = new AddAmenityToHotelCommand(request, hotelId, amenityId);
            var result = await Sender.Send(query, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{hotelId:guid}/amenities/{amenityId:guid}")]
        public async Task<IActionResult> RemoveAmenityFromHotel(Guid hotelId, Guid amenityId, CancellationToken cancellationToken)
        {
            var command = new RemoveAmenityFromHotelCommand(hotelId, amenityId);
            var result = await Sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{hotelId:guid}/amenities/{amenityId:guid}")]
        public async Task<IActionResult> UpdateHotelAmenity(Guid hotelId, Guid amenityId,
            [FromBody] double newPrice, CancellationToken cancellationToken)
        {
            var command = new UpdateHotelAmenityCommand(hotelId, amenityId, newPrice);
            var result = await Sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHotels(
            CancellationToken cancellationToken,
            // Pagination parameters with default values
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,

            // Filter parameters with default null values
            [FromQuery] Guid? countryId = null,
            [FromQuery] Guid? cityId = null,
            [FromQuery] Guid? localLocationId = null,
            [FromQuery] Guid? propertyTypeId = null,
            [FromQuery] Guid? amenityId = null,
            [FromQuery] double? minPrice = 0,
            [FromQuery] double? maxPrice = double.MaxValue,
            [FromQuery] double? minStarRate = 0,
            [FromQuery] double? maxStarRate = 5)
        {
            var query = new GetAllHotelsQuery(
                page,
                pageSize,
                countryId,
                cityId,
                localLocationId,
                propertyTypeId,
                amenityId,
                minPrice,
                maxPrice,
                minStarRate,
                maxStarRate);

            var result = await Sender.Send(query, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddHotel(AddHotelRequest hotelAddRequest, CancellationToken cancellationToken)
        {
            var command = new AddHotelCommand(hotelAddRequest);
            var result = await Sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return CreatedAtAction(nameof(GetHotelById), new { id = result.Value.Id }, result.Value);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateHotel([FromRoute] Guid id, [FromBody] UpdateHotelRequest updateHotelRequest, CancellationToken cancellationToken)
        {
            var command = new UpdateHotelCommand(id, updateHotelRequest);
            var result = await Sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteHotel(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteHotelCommand(id);
            var hotelResponse = await Sender.Send(command, cancellationToken);
            if (hotelResponse == null)
                return NotFound();
            return Ok(hotelResponse);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{hotelId:guid}/rooms")]
        public async Task<IActionResult> AddRoomToHotel(Guid hotelId, [FromBody] AddRoomToHotelRequest addRoomRequest, CancellationToken cancellationToken)
        {
            var command = new AddRoomToHotelCommand(hotelId, addRoomRequest);
            var result = await Sender.Send(command, cancellationToken);
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok(result.Value);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{hotelId:guid}/rooms/{roomId:guid}")]
        public async Task<IActionResult> RemoveRoomFromHotel(Guid hotelId, Guid roomId, CancellationToken cancellationToken)
        {
            var command = new RemoveRoomFromHotelCommand(hotelId, roomId);
            var result = await Sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{hotelId:guid}/images")]
        public async Task<IActionResult> UploadHotelImages(Guid hotelId, [FromForm] UploadImageApiRequest uploadImageApiRequest, CancellationToken cancellationToken)
        {

            var command = new UploadHotelImageCommand(hotelId, uploadImageApiRequest);

            var result = await Sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("image")]
        public async Task<IActionResult> RemoveHotelImages([FromBody] RemoveImageApiRequest removeImageApiRequest, CancellationToken cancellationToken)
        {

            var command = new RemoveHotelImageCommand(removeImageApiRequest.ImageUrl);

            var result = await Sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return NoContent();
        }

        [HttpGet("{hotelId:guid}/images")]
        public async Task<IActionResult> GetHotelImages(Guid hotelId, CancellationToken cancellationToken)
        {
            var query = new GetHotelImagesByHotelIdQuery(hotelId);
            var result = await Sender.Send(query, cancellationToken);

            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{hotelId:guid}/propertyType/{propertyTypeId:guid}")]
        public async Task<IActionResult> AssignPropertTypeToHotel(Guid hotelId, Guid propertyTypeId, CancellationToken cancellationToken)
        {
            var command = new AssignPropertyTypeToHotelCommand(hotelId, propertyTypeId);

            var result = await Sender.Send(command, cancellationToken);

            if (result.IsFailure) 
                return BadRequest(result.Error);

            return Ok(result.Value);
        }
    }
}