using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientDashboardAPI.Contracts;
using ClientDashboardAPI.Interfaces;
using ClientDashboardAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization.Internal;


namespace ClientDashboardAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class PhoneNumberController : ControllerBase
    {
        private readonly IPhoneNumberRepository _phoneNumberRepository;

        public PhoneNumberController(IPhoneNumberRepository phoneNumberRepository)
        {
            _phoneNumberRepository = phoneNumberRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePhoneNumber([FromBody] CreatePhoneNumberRequest phoneNumberRequest)
        {
            var phone = new PhoneNumber
            {
                ClientId = phoneNumberRequest.ClientId,
                CountryCode = phoneNumberRequest.CountryCode,
                Phone = phoneNumberRequest.PhoneNumber,
                NumberTypeId = phoneNumberRequest.PhoneNumberType,
                IsPrimary = phoneNumberRequest.IsPrimary.HasValue ? phoneNumberRequest.IsPrimary.Value : true
            };

            var newPhone = await _phoneNumberRepository.AddPhoneNumberAsync(phone);
            if (newPhone == null)
                return BadRequest("Error occurred adding phone number");

            var response = new PhoneNumberResponse
            {
                PhoneNumberId = newPhone.PhoneNumberId,
                ClientId = newPhone.ClientId,
                CountryCode = newPhone.CountryCode,
                PhoneNumber = newPhone.Phone,
                PhoneNumberType = newPhone.NumberTypeId,
                IsPrimary = newPhone.IsPrimary
            };

            return Ok(response);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePhoneNumber(int id, [FromBody] UpdatePhoneNumberRequest phoneNumberRequest)
        {
            var existingPhone = await _phoneNumberRepository.GetPhoneNumberAsync(id);
            if (existingPhone == null)
                return BadRequest("Unable to locate the phone number record in the database");

            var phone = new PhoneNumber
            {
                PhoneNumberId = id,
                ClientId = existingPhone.ClientId,
                CountryCode = phoneNumberRequest.CountryCode.HasValue ? phoneNumberRequest.CountryCode.Value : existingPhone.CountryCode,
                Phone = string.IsNullOrEmpty(phoneNumberRequest.PhoneNumber)==false ? phoneNumberRequest.PhoneNumber : existingPhone.Phone,
                NumberTypeId = phoneNumberRequest.PhoneNumberType.HasValue ? phoneNumberRequest.PhoneNumberType.Value : existingPhone.NumberTypeId,
                IsPrimary = phoneNumberRequest.IsPrimary.HasValue ? phoneNumberRequest.IsPrimary.Value : existingPhone.IsPrimary,

            };

            var result = await _phoneNumberRepository.UpdatePhoneNumberAsync(phone);
            if (result == null)
                return BadRequest("Error occurred during update");

            var response = new PhoneNumberResponse
            {
                PhoneNumberId = result.PhoneNumberId,
                ClientId = result.ClientId,
                CountryCode = result.CountryCode,
                PhoneNumber = result.Phone,
                PhoneNumberType = result.NumberTypeId,
                IsPrimary = result.IsPrimary
            };

            return Ok(response);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoneNumber(int id)
        {
            var deleted = await _phoneNumberRepository.DeletePhoneNumberAsync(id);
            if (deleted)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Error occurred while removing number");
            }
        }

        [HttpGet("client/{clientId}")]
        public async Task<IActionResult> GetClientPhoneNumbers(int clientId)
        {
            var clientNumbers = await _phoneNumberRepository.GetClientPhoneNumbersAsync(clientId);

            return Ok(clientNumbers);
        }
    }

}