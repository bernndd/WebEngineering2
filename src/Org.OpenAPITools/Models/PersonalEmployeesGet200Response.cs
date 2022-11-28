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
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Org.OpenAPITools.Converters;

namespace Org.OpenAPITools.Models
{ 
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class PersonalEmployeesGet200Response : IEquatable<PersonalEmployeesGet200Response>
    {
        /// <summary>
        /// list of all employees
        /// </summary>
        /// <value>list of all employees</value>
        [DataMember(Name="employees", EmitDefaultValue=false)]
        public List<Employee> Employees { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class PersonalEmployeesGet200Response {\n");
            sb.Append("  Employees: ").Append(Employees).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((PersonalEmployeesGet200Response)obj);
        }

        /// <summary>
        /// Returns true if PersonalEmployeesGet200Response instances are equal
        /// </summary>
        /// <param name="other">Instance of PersonalEmployeesGet200Response to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PersonalEmployeesGet200Response other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    Employees == other.Employees ||
                    Employees != null &&
                    other.Employees != null &&
                    Employees.SequenceEqual(other.Employees)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                // Suitable nullity checks etc, of course :)
                    if (Employees != null)
                    hashCode = hashCode * 59 + Employees.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(PersonalEmployeesGet200Response left, PersonalEmployeesGet200Response right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PersonalEmployeesGet200Response left, PersonalEmployeesGet200Response right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}