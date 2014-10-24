using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspSiteBase.Models
{
    public enum StatusTypes {
        Active,
        Deactive,
        Deleted
    }
    public class Contactus
    {
        public int ContactusId { get; set; }
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Msg { get; set; }
        public StatusTypes Status {get; set;}

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime Created { get; set; }
        public string Ip { get; set; }
    }
}