//
//  ServiceCollectionExtensions.cs
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

using System.Text.Json;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Remora.OAuth2.Abstractions;
using Remora.OAuth2.Abstractions.OAuthExtensions.DeviceAuthorizationGrant;
using Remora.OAuth2.Abstractions.OAuthExtensions.TokenRevocation;
using Remora.OAuth2.Json.Internal;
using Remora.OAuth2.OAuth2Extensions.DeviceAuthorizationGrant;
using Remora.OAuth2.OAuth2Extensions.TokenRevocation;
using Remora.Rest.Extensions;
using Remora.Rest.Json;
using Remora.Rest.Json.Policies;

namespace Remora.OAuth2.Extensions;

/// <summary>
/// Defines extensions to the <see cref="IServiceCollection"/> interface.
/// </summary>
[PublicAPI]
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the required services and data converters to work with JSON data to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection, with the services added.</returns>
    public static IServiceCollection AddOAuth2(this IServiceCollection services)
    {
        services.ConfigureRestJsonConverters("Remora.OAuth2");

        return services.Configure<JsonSerializerOptions>
        (
            "Remora.OAuth2",
            options =>
            {
                options.PropertyNamingPolicy = new SnakeCaseNamingPolicy();

                options
                    .AddCoreOAuth2Converters()
                    .AddDeviceAuthorizationConverters()
                    .AddTokenRevocationConverters();
            }
        );
    }

    private static JsonSerializerOptions AddCoreOAuth2Converters(this JsonSerializerOptions options)
    {
        options.AddDataObjectConverter
        <
            IAuthorizationCodeAccessTokenResponse,
            AuthorizationCodeAccessTokenResponse
        >()
        .WithPropertyConverter(r => r.Scope, new SpaceSeparatedListConverter())
        .WithPropertyConverter(r => r.ExpiresIn, new UnitTimeSpanConverter(TimeUnit.Seconds));

        options.AddDataObjectConverter
        <
            IAuthorizationCodeAccessTokenErrorResponse,
            AuthorizationCodeAccessTokenErrorResponse
        >();

        options.AddDataObjectConverter
        <
            IResourceOwnerPasswordCredentialsAccessTokenResponse,
            ResourceOwnerPasswordCredentialsAccessTokenResponse
        >()
        .WithPropertyConverter(r => r.Scope, new SpaceSeparatedListConverter())
        .WithPropertyConverter(r => r.ExpiresIn, new UnitTimeSpanConverter(TimeUnit.Seconds));

        options.AddDataObjectConverter
        <
            IResourceOwnerPasswordCredentialsAccessTokenErrorResponse,
            ResourceOwnerPasswordCredentialsAccessTokenErrorResponse
        >();

        options.AddDataObjectConverter
        <
            IClientCredentialsAccessTokenResponse,
            ClientCredentialsAccessTokenResponse
        >()
        .WithPropertyConverter(r => r.Scope, new SpaceSeparatedListConverter())
        .WithPropertyConverter(r => r.ExpiresIn, new UnitTimeSpanConverter(TimeUnit.Seconds));

        options.AddDataObjectConverter
        <
            IClientCredentialsAccessTokenErrorResponse,
            ClientCredentialsAccessTokenErrorResponse
        >();

        options.AddDataObjectConverter
        <
            IRefreshAccessTokenResponse,
            RefreshAccessTokenResponse
        >()
        .WithPropertyConverter(r => r.Scope, new SpaceSeparatedListConverter())
        .WithPropertyConverter(r => r.ExpiresIn, new UnitTimeSpanConverter(TimeUnit.Seconds));

        options.AddDataObjectConverter
        <
            IRefreshAccessTokenErrorResponse,
            RefreshAccessTokenErrorResponse
        >();

        return options;
    }

    private static JsonSerializerOptions AddDeviceAuthorizationConverters(this JsonSerializerOptions options)
    {
        options.AddDataObjectConverter
        <
            IDeviceAuthorizationAuthorizationResponse,
            DeviceAuthorizationAuthorizationResponse
        >()
        .WithPropertyConverter(r => r.ExpiresIn, new UnitTimeSpanConverter(TimeUnit.Seconds))
        .WithPropertyConverter(r => r.Interval, new UnitTimeSpanConverter(TimeUnit.Seconds));

        options.AddDataObjectConverter
        <
            IDeviceAuthorizationAuthorizationErrorResponse,
            DeviceAuthorizationAuthorizationErrorResponse
        >();

        options.AddDataObjectConverter
        <
            IDeviceAuthorizationAccessTokenResponse,
            DeviceAuthorizationAccessTokenResponse
        >()
        .WithPropertyConverter(r => r.Scope, new SpaceSeparatedListConverter())
        .WithPropertyConverter(r => r.ExpiresIn, new UnitTimeSpanConverter(TimeUnit.Seconds));

        options.AddDataObjectConverter
        <
            IDeviceAuthorizationAccessTokenErrorResponse,
            DeviceAuthorizationAccessTokenErrorResponse
        >();

        return options;
    }

    private static JsonSerializerOptions AddTokenRevocationConverters(this JsonSerializerOptions options)
    {
        options.AddDataObjectConverter
        <
            ITokenRevocationErrorResponse,
            TokenRevocationErrorResponse
        >();

        return options;
    }
}
