﻿using System.Collections.Generic;
using System.Linq;
using BuildTheLanesAPI.Entities;
using BuildTheLanesAPI.Models;

namespace BuildTheLanesAPI.Helpers
{
    public static class ExtensionMethods
    {
        public static IEnumerable<RegisterModel> WithoutPasswords(this IEnumerable<RegisterModel> users)
        {
            if (users == null) return null;

            return users.Select(x => x.WithoutPassword());
        }

        public static RegisterModel WithoutPassword(this RegisterModel user)
        {
            if (user == null) return null;

            user.Password = null;
            return user;
        }
    }
}