/*
 * Biletado services
 *
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 1.0.0
 * Contact: dh@blaimi.de
 * Generated by: https://openapi-generator.tech
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using Org.OpenAPITools.Attributes;
using Org.OpenAPITools.Models;
using Npgsql;

namespace Org.OpenAPITools.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class DefaultApiController : ControllerBase
    { 
        /// <summary>
        /// returns information about the backend-service and status
        /// </summary>
        /// <response code="200">successful operation</response>
        [HttpGet]
        [Route("/personal/status/")]
        [ValidateModelState]
        [SwaggerOperation("PersonalStatusGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(PersonalStatusGet200Response), description: "successful operation")]
        public virtual IActionResult PersonalStatusGet()
        {
            var cs = "Server=localhost;Database=personal;Port=5432;UserId=postgres;Password=postgres";
            using var con = new NpgsqlConnection(cs);
            con.Open();

            var sql = "SELECT version()";

            using var cmd = new NpgsqlCommand(sql, con);

            var version = cmd.ExecuteScalar().ToString();
            Console.WriteLine($"PostgreSQL version: {version}");

            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(PersonalStatusGet200Response));
            string exampleJson = null;
            exampleJson = "{\n  \"authors\" : [ \"Simon\", \"Bernd\" ]\n}";
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<PersonalStatusGet200Response>(exampleJson)
            : default(PersonalStatusGet200Response);
            //TODO: Change the data returned
            return new ObjectResult(example);
        }
    }
}
