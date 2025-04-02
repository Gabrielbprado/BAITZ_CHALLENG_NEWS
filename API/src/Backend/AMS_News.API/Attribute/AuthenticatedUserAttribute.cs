using AMS_News.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace AMS_News.API.Attribute;

public class AuthenticatedUserAttribute() : TypeFilterAttribute(typeof(AuthenticatedUserAttributeFilter));
    
