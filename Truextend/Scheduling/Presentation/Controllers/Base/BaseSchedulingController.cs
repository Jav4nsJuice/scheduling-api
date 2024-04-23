using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Truextend.Scheduling.Logic.Managers.Base;
using Truextend.Scheduling.Presentation.Middleware;

namespace Truextend.Scheduling.Presentation.Controllers.Base
{
	[ApiController]
	public class BaseSchedulingController<T> : ControllerBase where T : class
	{
        private readonly IGenericManager<T> _classManager;

        public BaseSchedulingController(IGenericManager<T> ClassManager)
		{
			_classManager = ClassManager;
		}

        /// <summary>
        /// Returns all Items according to the endpoint.
        /// </summary>

        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Conflict)]
        [HttpGet]
        public virtual async Task<IActionResult> GetAllItems()
        {
            return Ok(new MiddlewareResponse<IEnumerable<T>>(await _classManager.GetAll()));
        }

        /// <summary>
        /// Returns specific Item from the Id provided.
        /// </summary>
        /// <param name="id">Id of the Item</param>

        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Conflict)]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetItemById([FromRoute] Guid id)
        {
            T response = await _classManager.GetById(id);
            return Ok(new MiddlewareResponse<T>(response));
        }

        /// <summary>
        /// Creates an Item.
        /// </summary>
        /// <remarks>
        /// When creating the request body you can ignore the value of the Id parameter, it will be generated automatically.
        /// </remarks>

        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Conflict)]
        [HttpPost]
        [Route("")]
        public virtual async Task<IActionResult> AddItem([FromBody] T itemDto)
        {
            T response = await _classManager.Create(itemDto);
            return Ok(new MiddlewareResponse<T>(response));
        }

        /// <summary>
        /// Updates the Item of the provided Id.
        /// </summary>
        /// <param name="id">Id of the Item</param>
        /// <param name="itemDto"></param>

        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Conflict)]
        [HttpPut("{id}")]
        public virtual async Task<IActionResult> UpdateById([FromRoute] Guid id, [FromBody] T itemDto)
        {
            T response = await _classManager.Update(itemDto, id);
            return Ok(new MiddlewareResponse<T>(response));
        }

        /// <summary>
        /// Deletes the Item of the provided Id.
        /// </summary>
        /// <param name="id">Id of the item</param>

        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Conflict)]
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            return Ok(new MiddlewareResponse<bool>(await _classManager.Delete(id)));
        }
    }
}

