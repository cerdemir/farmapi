using System;

namespace farmapi.Swagger
{
    public class DescriptionLoader
    {
        internal static string LoadApiDescription()
        {
            var description = string.Empty;
            try
            {
                var assembly = typeof(Startup).Assembly;
                const string documentDescriptionPath = "farmapi.Swagger.apidescription.md";
                using(var reader = new System.IO.StreamReader(assembly.GetManifestResourceStream(documentDescriptionPath)))
                {
                    description = reader.ReadToEnd();
                }
            }
            catch
            {
                //shallow
            }
            return description;
        }
    }
}