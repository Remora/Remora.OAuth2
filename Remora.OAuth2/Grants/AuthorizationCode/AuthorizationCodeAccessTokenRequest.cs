//
//  AuthorizationCodeAccessTokenRequest.cs
//
//  Author:
//       Jarl Gullberg <jarl.gullberg@gmail.com>
//
//  Copyright (c) 2017 Jarl Gullberg
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
using System.Collections.Generic;
using System.Web;
using JetBrains.Annotations;
using Remora.OAuth2.Abstractions;
using Remora.OAuth2.Abstractions.OAuthExtensions;
using Remora.OAuth2.Extensions;
using Remora.Rest.Core;

namespace Remora.OAuth2;

/// <inheritdoc />
[PublicAPI]
public record AuthorizationCodeAccessTokenRequest
(
    string Code,
    Optional<Uri> RedirectUri = default,
    Optional<string> ClientID = default,
    Optional<IReadOnlyList<IAccessTokenRequestExtension>> Extensions = default
) : IAuthorizationCodeAccessTokenRequest
{
    /// <inheritdoc />
    public Uri ToRequestUri(Uri tokenEndpoint)
    {
        var builder = new UriBuilder(tokenEndpoint);

        var queryParameters = HttpUtility.ParseQueryString(tokenEndpoint.Query);
        queryParameters.Add("grant_type", ((IAccessTokenRequest)this).GrantType);
        queryParameters.Add("code", this.Code);
        queryParameters.Add("redirect_uri", this.RedirectUri);
        queryParameters.Add("client_id", this.ClientID);

        if (this.Extensions.IsDefined(out var extensions))
        {
            foreach (var requestExtension in extensions)
            {
                requestExtension.AddParameters(queryParameters);
            }
        }

        builder.Query = queryParameters.ToString();
        return builder.Uri;
    }
}
