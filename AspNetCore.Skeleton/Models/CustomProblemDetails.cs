using AspNetCore.Skeleton.Extensions.IApplicationBuilderExtensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace AspNetCore.Skeleton.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomProblemDetails : ProblemDetails
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomProblemDetails()
        {

        }
        /// <summary>
        /// Constructor with params
        /// </summary>
        /// <param name="title">Title of the error</param>
        /// <param name="status">Http status of the error</param>
        /// <param name="detail">Detail of the error</param>
        public CustomProblemDetails(string title, int? status = null, string detail = null)
        {
            this.Title = title;
            this.Status = status;
            this.Detail = detail;
        }

        /// <summary>
        /// A URI reference that identifies the specific occurrence of the problem.It may or may not yield further information if dereferenced.
        /// </summary>
        public new string Instance { get; } = $"urn:{ApplicationBuilderExceptionMiddlewareExtension.AppName}:error:{Guid.NewGuid()}";

        /// <summary>
        /// Return a json formated string
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }

    }
}
