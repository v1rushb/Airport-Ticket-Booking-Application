using System;
using System.ComponentModel.DataAnnotations;

namespace Airport.Utilties {
    public class FutureDateAttribute : ValidationAttribute {
        public override bool IsValid(object val) {
            if(val is DateTime date) { 
                return date >= DateTime.Today;
            }
            return false;
        }
    }
}