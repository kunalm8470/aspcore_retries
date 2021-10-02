# Retry pattern using Polly in ASP .NET Core

The Polly .NET library helps simplify retries by abstracting away the retry logic, allowing you to focus on your own code. You can do retries with and without delays.

To install, paste this in Nuget Package Manager console -
> Install-Package Polly

To simulate rate limiting we make use of `Random.Next` in the server side backend controller action.

The client uses Polly .NET as common retry logic along with exponential backoff of `2^Attempt seconds` along with an additional parameter called as [`jitter`](https://github.com/App-vNext/Polly/wiki/Retry-with-jitter). This jitter is calculated thread-safe using `Random.Next` and the resultant jitter is added to the backoff as milliseconds.

All these steps are done to make our backend service more resilient to DoS attacks.
