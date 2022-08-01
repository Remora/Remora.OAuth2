//
//  ResourceOwnerPasswordCredentialsAccessTokenRequest.cs
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
using System.Net.Http;
using System.Web;
using JetBrains.Annotations;
using Remora.OAuth2.Abstractions;
using Remora.OAuth2.Abstractions.OAuthExtensions;
using Remora.OAuth2.Extensions;
using Remora.Rest.Core;

namespace Remora.OAuth2;

/// <inheritdoc />
[PublicAPI]
public record ResourceOwnerPasswordCredentialsAccessTokenRequest
(
    string Username,
    string Password,
    Optional<string> Scope = default,
    Optional<IReadOnlyList<IAccessTokenRequestExtension>> Extensions = default
) : IResourceOwnerPasswordCredentialsAccessTokenRequest
{
    /// <inheritdoc />
    public HttpRequestMessage ToRequest(Uri tokenEndpoint)
    {
        var parameters = new Dictionary<string, string>
        {
            { "grant_type", ((IAccessTokenRequest)this).GrantType },
            { "username", this.Username },
            { "password", this.Password },
            { "scope", this.Scope }
        };

        // ReSharper disable once InvertIf
        if (this.Extensions.IsDefined(out var extensions))
        {
            foreach (var requestExtension in extensions)
            {
                requestExtension.AddParameters(parameters);
            }
        }

        return new HttpRequestMessage(HttpMethod.Post, tokenEndpoint)
        {
            Content = new FormUrlEncodedContent(parameters)
        };
    }
}
