# keycloak-user-group-query
Demonstrates how the [Keycloak Admin REST API](https://www.keycloak.org/docs-api/21.0.1/rest-api/index.html#_overview) can be used to query user groups and list group members with a **service account** for security reasons.

This approach was tested with the Keycloak version `18.0.0`.

## Keycloak setup
Create a new client in your Keycloak realm with the following settings:
- *Enabled*: on
- *Client Protocol*: openid-connect
- *Access Type*: confidential
- *Direct Access Grants Enabled*: on
- *Service Accounts Enabled*: on

After that, hit "Save" and copy the client secret from the "Credentials" tab for the `launchSettings.json` configuration.  

You should now also have a "Service Account Roles" tab in your Keycloak client page.  
Here you have to go to "Service Account Roles" -> "Client Roles" and select `realm-management`.  

The following roles must be assigned to the service user:
- `query-groups`
- `view-users`

That's it. You can now set the environment variables in the `launchSettings.json` file and execute the program with `dotnet run`.
