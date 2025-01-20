using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace StockPortfolioAPI.Extension
{
    public static class DTOtoPOCOExtension
    {
        public static POCO Convert<POCO>(this object dto)
        {
            // Get the type of the POCO (Plain Old CLR Object)
            Type pocoType = typeof(POCO);
            // Create an instance of the POCO
            POCO pocoInstance = (POCO)Activator.CreateInstance(pocoType);

            // Get all properties of the DTO
            PropertyInfo[] dtoProperties = dto.GetType().GetProperties();
            // Get all properties of the POCO
            PropertyInfo[] pocoProperties = pocoType.GetProperties();

            // Iterate through each property of the DTO
            foreach (PropertyInfo dtoProperty in dtoProperties)
            {
                // Find the corresponding property in the POCO with the same name
                PropertyInfo pocoProperty = Array.Find(pocoProperties, p => p.Name == dtoProperty.Name);

                // If a matching property is found and the types are compatible, copy the value from the DTO to the POCO
                if (pocoProperty != null && dtoProperty.PropertyType == pocoProperty.PropertyType)
                {
                    // Get the value of the property from the DTO
                    object value = dtoProperty.GetValue(dto);
                    // Set the value in the POCO object
                    pocoProperty.SetValue(pocoInstance, value);
                }
            }
            // Return the newly created POCO object with the copied values from the DTO
            return pocoInstance;
        }
    }
}