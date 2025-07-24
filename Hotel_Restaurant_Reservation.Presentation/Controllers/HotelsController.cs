using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.AddHotel;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.DeleteHotel;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.UpdateHotel;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries.GetAllHotels;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries.GetFilteredHotels;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries.GetHotelById;
using Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands;
using Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands.UploadHotelImages;
using Hotel_Restaurant_Reservation.Application.Implementation.Images.Queries.GetHotelImagesByHotelId;
using Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Commands.AddRoom;
using Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Commands.AddRoomToHotel;
using Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Commands.RemoveRoomFromHotel;
using Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Queries.GetRoomsByHotelId;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
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

        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> GetAllHotel(CancellationToken cancellationToken)
        {
            var query = new GetAllHotelsQuery();
            var result = await Sender.Send(query, cancellationToken);

            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFilteredHotels(
            [FromQuery] Guid? countryId,
            [FromQuery] Guid? cityId,
            [FromQuery] Guid? localLocationId,
            [FromQuery] Guid? propertyTypeId,
            [FromQuery] Guid? amenityId,
            [FromQuery] double? minPrice,
            [FromQuery] double? maxPrice,
            [FromQuery] double? minStarRate,
            [FromQuery] double? maxStarRate,
            CancellationToken cancellationToken)
        {
            var query = new GetFilteredHotelsQuery
            {
                CountryId = countryId,
                CityId = cityId,
                LocalLocationId = localLocationId,
                PropertyTypeId = propertyTypeId,
                AmenityId = amenityId,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                MinStarRate = minStarRate,
                MaxStarRate = maxStarRate
            };
            var result = await Sender.Send(query, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> AddHotel(AddHotelRequest hotelAddRequest, CancellationToken cancellationToken)
        {
            var command = new AddHotelCommand(hotelAddRequest);
            var result = await Sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return CreatedAtAction(nameof(GetHotelById), new { id = result.Value.Id }, result.Value);
        }

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

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteHotel(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteHotelCommand(id);
            var hotelResponse = await Sender.Send(command, cancellationToken);
            if (hotelResponse == null)
                return NotFound();
            return Ok(hotelResponse);
        }

        [HttpPost("{hotelId:guid}/rooms")]
        public async Task<IActionResult> AddRoomToHotel(Guid hotelId, [FromBody] AddRoomToHotelRequest addRoomRequest, CancellationToken cancellationToken)
        {
            var command = new AddRoomToHotelCommand(hotelId, addRoomRequest);
            var result = await Sender.Send(command, cancellationToken);
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok(result.Value);
        }

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

        [HttpPost("{hotelId:guid}/images")]
        public async Task<IActionResult> UploadHotelImages(Guid hotelId, [FromForm] List<UploadImageRequest> imageFiles, CancellationToken cancellationToken)
        {
            if (imageFiles == null || imageFiles.Count == 0)
            {
                return BadRequest("No files were uploaded.");
            }

            var command = new UploadHotelImagesCommand
            {
                HotelId = hotelId,
                ImageFiles = imageFiles
            };

            var result = await Sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
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
    }
}