using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StockPortfolioAPI.Models.DTO
{
    public class DTOPRT01
    {
        [Required(ErrorMessage = "Portfolio Name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Portfolio Name must be between 3 and 100 characters.")]
        [JsonProperty("T01103")]
        public string T01F03 { get; set; }
    }
}