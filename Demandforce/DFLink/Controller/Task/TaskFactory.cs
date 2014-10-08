// -----------------------------------------------------------------------
// <copyright file="TaskFactory.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Controller.Task
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// This class is to create different task instance
    /// </summary>
    public class TaskFactory : ITaskFactory
    {
        /// <summary>
        /// Create the different task instance according taskName
        /// </summary>
        /// <param name="taskName">The Task name as the key to look up</param>
        /// <returns>The instance of task</returns>
        public ITaskMaker CreateTaskMaker(string taskName)
        {
            var instance = Factory<string, object>.CreateInstance(taskName);

            if (instance != null)
            {
                return (ITaskMaker)instance;
            }

            // Get the type name from the configuration file
            var typeName = this.GetTypeNameFromConfiguration(taskName);

            // Create a type
            var type = Type.GetType(typeName);
            Factory<string, object>.Add(taskName, type);
            instance = Factory<string, object>.CreateInstance(taskName);

            return (ITaskMaker)instance;
        }

        /// <summary>
        /// Get the type name from configuration file
        /// </summary>
        /// <param name="name">The unique type name</param>
        /// <returns>The type name with qualified assembly</returns>
        public string GetTypeNameFromConfiguration(string name)
        {
            // Get task intance configuration using local file, will get from web service
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app.config"); 
            var fileMap = new ConfigurationFileMap(filePath);
            Configuration configFile = ConfigurationManager.OpenMappedMachineConfiguration(fileMap);
            var settings = configFile.AppSettings.Settings;        
            if (settings != null)
            {
                foreach (var key in settings.AllKeys.Where(key => key == name))
                {
                    return settings[key].Value;
                }
            }

            return string.Empty;
        }
    }
}
