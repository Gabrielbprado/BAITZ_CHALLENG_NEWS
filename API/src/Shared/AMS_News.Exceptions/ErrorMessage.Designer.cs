﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AMS_News.Exceptions {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ErrorMessage {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public ErrorMessage() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("AMS_News.Exceptions.ErrorMessage", typeof(ErrorMessage).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to the email already exists.
        /// </summary>
        public static string EMAIL_ALREADY_EXISTS {
            get {
                return ResourceManager.GetString("EMAIL_ALREADY_EXISTS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to the email cannot be null.
        /// </summary>
        public static string EMAIL_EMPTY {
            get {
                return ResourceManager.GetString("EMAIL_EMPTY", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to the email is invalid.
        /// </summary>
        public static string EMAIL_INVALID {
            get {
                return ResourceManager.GetString("EMAIL_INVALID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to the password is incorrect.
        /// </summary>
        public static string INCORRECT_PASSWORD {
            get {
                return ResourceManager.GetString("INCORRECT_PASSWORD", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to email or password invalid.
        /// </summary>
        public static string INVALID_LOGIN {
            get {
                return ResourceManager.GetString("INVALID_LOGIN", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to the name cannot be null.
        /// </summary>
        public static string NAME_EMPTY {
            get {
                return ResourceManager.GetString("NAME_EMPTY", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Token can&apos;t be empty.
        /// </summary>
        public static string NO_TOKEN {
            get {
                return ResourceManager.GetString("NO_TOKEN", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to the password cannot be null.
        /// </summary>
        public static string PASSWORD_EMPTY {
            get {
                return ResourceManager.GetString("PASSWORD_EMPTY", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to the password must be at least 6 characters long.
        /// </summary>
        public static string PASSWORD_INVALID {
            get {
                return ResourceManager.GetString("PASSWORD_INVALID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unknown Error.
        /// </summary>
        public static string UNKNOWN_ERROR {
            get {
                return ResourceManager.GetString("UNKNOWN_ERROR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to user have&apos;t permission.
        /// </summary>
        public static string USER_WITHOUT_PERMISSION {
            get {
                return ResourceManager.GetString("USER_WITHOUT_PERMISSION", resourceCulture);
            }
        }
    }
}
