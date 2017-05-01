using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RubenAddressClientWepApp.Models;
using System;

namespace RubenAddressClientWepApp.Controllers
{
    [Route("api/[controller]")]
    public class ClientAddressController : Controller
    {
        private readonly IClientAddressRepo _clientAddressRepository;
        protected LogMessageFromRepo logMessage = new LogMessageFromRepo();

        public ClientAddressController(IClientAddressRepo clientAddressRepository)
        {
            _clientAddressRepository = clientAddressRepository;
        }

        [HttpGet]
        public IEnumerable<ClientAddressItem> GetAll()
        {
            return _clientAddressRepository.GetAll();
        }

        [HttpGet("{id}", Name = "GetAddressClient")]
        public IActionResult GetById(int id)
        {
            var item = _clientAddressRepository.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ClientAddressItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            //en add se define como active true y el dateStamp como datetime.now
            item.Active = true;
            item.DateStamp = DateTime.UtcNow;            
            logMessage = _clientAddressRepository.Add(item);

            if (!logMessage.Success)
            {
                return BadRequest(logMessage.LogMessage);
            }

            return CreatedAtRoute("GetAddressClient", new { id = item.ID }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ClientAddressItem item)
        {
            if (item == null || item.ID != id)
            {
                return BadRequest();
            }

            var clientAddress = _clientAddressRepository.Find(id);
            if (clientAddress == null)
            {
                return NotFound();
            }

            clientAddress.City = item.City;
            clientAddress.DateStamp = DateTime.UtcNow;
            clientAddress.Active = true;
            clientAddress.Intersection1 = item.Intersection1;
            clientAddress.State = item.State;
            clientAddress.Street = item.Street;
            clientAddress.Zip = item.Zip;

            logMessage = _clientAddressRepository.Update(clientAddress);

            if (!logMessage.Success)
            {
                return BadRequest(logMessage.LogMessage);
            }
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var clientAddress = _clientAddressRepository.Find(id);
            if (clientAddress == null)
            {
                return NotFound();
            }

            logMessage = _clientAddressRepository.Remove(id);

            if (!logMessage.Success)
            {
                return BadRequest(logMessage.LogMessage);
            }

            return new NoContentResult();
        }

    }
}