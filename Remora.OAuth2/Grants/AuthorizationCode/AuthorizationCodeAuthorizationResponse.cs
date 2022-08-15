//
//  AuthorizationCodeAuthorizationResponse.cs
//
//  Author:
//       Jarl Gullberg <jarl.gullberg@gmail.com>
//
//  Copyright (c) Jarl Gullberg
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using JetBrains.Annotations;
using Remora.OAuth2.Abstractions;
using Remora.OAuth2.Extensions;
using Remora.Rest.Core;

namespace Remora.OAuth2;

/// <inheritdoc />
[PublicAPI]
public record AuthorizationCodeAuthorizationResponse(string Code, Optional<string> State = default)
    : IAuthorizationCodeAuthorizationResponse
{
    /// <summary>
    /// Attempts to parse an authorization response from the given URI, visited by the user agent.
    /// </summary>
    /// <param name="location">The visited URI.</param>
    /// <param name="response">The parsed response.</param>
    /// <returns>true if a valid response was parsed; otherwise, false.</returns>
    public static bool TryParse(Uri location, [NotNullWhen(true)] out IAuthorizationCodeAuthorizationResponse? response)
    {
        response = null;

        var properties = HttpUtility.ParseQueryString(location.Query).ToDictionary();
        if (!properties.TryGetValue("code", out var code))
        {
            return false;
        }

        _ = properties.TryGetValue("state", out Optional<string> state);

        response = new AuthorizationCodeAuthorizationResponse(code, state);
        return true;
    }
}
