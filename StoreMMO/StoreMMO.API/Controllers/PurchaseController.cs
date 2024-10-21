using Azure;
using BusinessLogic.Services.StoreMMO.Core.Purchases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.payOS;
using StoreMMO.Core.ViewModels;
using Net.payOS.Types;

namespace StoreMMO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchase;
        private readonly PayOS _payos;
        public PurchaseController(IPurchaseService purchase ,PayOS payOS)
        {
            this._purchase = purchase;
            this._payos = payOS;
        }

 
        [HttpPost("Add")]
        public IActionResult Add(OrderBuyViewModels orderBuyViewModels)
        {
            if (this._purchase.add(orderBuyViewModels))
            {
                return Ok(new { message = "Order added successfully." });
            }
            return BadRequest(new { message = "Failed to add order." });
        }

        // DELETE: api/Purchase/Delete
        [HttpDelete("Delete")]
        public IActionResult Delete(OrderBuyViewModels orderBuyViewModels)
        {
            if (_purchase.Delete(orderBuyViewModels))
            {
                return Ok(new { message = "Order deleted successfully." });
            }
            return BadRequest(new { message = "Failed to delete order." });
        }

        // PUT: api/Purchase/Edit
        [HttpPut("Edit")]
        public IActionResult Edit(OrderBuyViewModels orderBuyViewModels)
        {
            if (_purchase.Edit(orderBuyViewModels))
            {
                return Ok(new { message = "Order edited successfully." });
            }
            return BadRequest(new { message = "Failed to edit order." });
        }

        [HttpPut("Update")]
        public IActionResult Update(OrderBuyViewModels orderBuyViewModels)
        {
            if (_purchase.Update(orderBuyViewModels))
            {
                return Ok(new { message = "Order updated successfully." });
            }
            return BadRequest(new { message = "Failed to update order." });
        }

        [HttpGet("GetByID/{id}")]
        public IActionResult GetByID(string id)
        {
            var order = _purchase.GetByID(id);
            if (order != null)
            {
                return Ok(order);
            }
            return NotFound(new { message = "Order not found." });
        }

        [HttpGet("CheckOrder/{ordercode}")]
        public async Task<IActionResult> CheckOrder(long ordercode)
        {
            try
            {
                var order = await this._payos.getPaymentLinkInformation(ordercode);
                if (order != null)
                {
                    return Ok(order);
                }
                return NotFound(new { message = "Order not found." });
            }
            catch(Exception e)
            {
                return NotFound(new { message = "Order not found." });
            }
        }
    }
}
