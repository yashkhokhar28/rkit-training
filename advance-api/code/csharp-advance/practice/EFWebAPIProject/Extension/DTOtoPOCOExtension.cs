﻿using System;
using System.Reflection;

namespace EFWebAPIProject.Extension
{
    /// <summary>
    /// Provides extension methods for converting a DTO (Data Transfer Object) to a POCO (Plain Old CLR Object).
    /// </summary>
    public static class DTOtoPOCOExtension
    {
        /// <summary>
        /// Converts a DTO object to a POCO object of the specified type.
        /// </summary>
        /// <typeparam name="POCO">The type of the POCO to convert the DTO to.</typeparam>
        /// <param name="dto">The DTO object to be converted.</param>
        /// <returns>A POCO object with the same property values as the DTO.</returns>
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