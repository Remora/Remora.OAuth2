//
//  DeviceAuthorizationAccessTokenRequestTestsBase.cs
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
using Remora.OAuth.Tests.Fakes;
using Remora.OAuth2.Abstractions;
using Remora.OAuth2.Abstractions.OAuthExtensions;
using Remora.OAuth2.Abstractions.OAuthExtensions.DeviceAuthorizationGrant;
using Remora.Rest.Xunit.Extensions;
using Xunit;

namespace Remora.OAuth.Tests.TestBases;

/// <summary>
/// Serves as a test base for test classes that handle device authorization requests.
/// </summary>
/// <typeparam name="TAuthorizationRequest">The request type.</typeparam>
public abstract class DeviceAuthorizationAccessTokenRequestTestsBase<TAuthorizationRequest>
    where TAuthorizationRequest : IDeviceAuthorizationAccessTokenRequest
{
    /// <summary>
    /// Gets the expected grant type.
    /// </summary>
    protected abstract string GrantType { get; }

    /// <summary>
    /// Gets a function that creates a blank request with no special data in it.
    /// </summary>
    protected abstract Func<TAuthorizationRequest> BlankRequestFactory { get; }

    /// <summary>
    /// Gets a function that creates a blank request with extension data in it.
    /// </summary>
    protected abstract Func<IDeviceAuthorizationAccessTokenRequestExtension, TAuthorizationRequest> ExtendedRequestFactory { get; }

    /// <summary>
    /// Gets the expected HTTP method that the request should use.
    /// </summary>
    protected virtual HttpMethod ExpectedHttpMethod => HttpMethod.Post;

    /// <summary>
    /// Tests whether the created request includes the correct grant type.
    /// </summary>
    [Fact]
    public void CreatesRequestWithCorrectGrantType()
    {
        var request = this.BlankRequestFactory();
        var message = request.ToRequest(new Uri("about:blank"));

        Assert.Equal(this.GrantType, request.GrantType);
        message.HasUrlEncodedFormData(new Dictionary<string, string>
        {
            { "grant_type", request.GrantType }
        });
    }

    /// <summary>
    /// Tests whether the created request uses the correct HTTP method.
    /// </summary>
    [Fact]
    public void CreatesRequestWithCorrectHttpMethod()
    {
        var request = this.BlankRequestFactory();
        var message = request.ToRequest(new Uri("about:blank"));

        Assert.Equal(this.ExpectedHttpMethod, message.Method);
    }

    /// <summary>
    /// Tests whether the created request uses the correct request URI.
    /// </summary>
    [Fact]
    public void CreatesRequestWithCorrectRequestUri()
    {
        var expected = new Uri("https://unit-test.net");

        var request = this.BlankRequestFactory();
        var message = request.ToRequest(expected);

        Assert.Equal(expected.AbsolutePath, message.RequestUri?.AbsolutePath);
    }

    /// <summary>
    /// Tests whether the created request retains additional query parameters in the request URI.
    /// </summary>
    [Fact]
    public void RetainsAdditionalQueryParameters()
    {
        var expected = new Uri("https://unit-test.net?parameter=value&other=something");

        var request = this.BlankRequestFactory();
        var message = request.ToRequest(expected);

        message.HasQueryParameters(new Dictionary<string, string>
        {
            { "parameter", "value" },
            { "other", "something" },
        });
    }

    /// <summary>
    /// Tests whether the created request includes extension properties.
    /// </summary>
    [Fact]
    public void CanCreateRequestWithExtensionProperties()
    {
        var request = this.ExtendedRequestFactory(new FakeExtension());
        var message = request.ToRequest(new Uri("about:blank"));

        message.HasUrlEncodedFormData(new Dictionary<string, string>
        {
            { FakeExtension.Name, FakeExtension.Value }
        });
    }
}
