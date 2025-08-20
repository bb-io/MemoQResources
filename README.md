# Blackbird.io memoQ Server Resources

Blackbird is the new automation backbone for the language technology industry. Blackbird provides enterprise-scale automation and orchestration with a simple no-code/low-code platform. Blackbird enables ambitious organizations to identify, vet and automate as many processes as possible. Not just localization workflows, but any business and IT process. This repository represents an application that is deployable on Blackbird and usable inside the workflow editor.

<!-- begin docs -->

## Introduction

The memoQ Server Resources API is currently the API supports manipulating translation memories (TM) and termbases (TB).


## Before setting up

Before you can connect you need to make sure that:

- You have a memoQ server installed and running.
- You have a user account on the memoQ server.
- You have the memoQ server API enabled.

## Setting up

1. Open the Blackbird.io platform.
2. Go to the `Apps` section and search for memoQ Resources.
3. Click on the `Add Connection` button.
4. Name your connection.
5. Fill in the `Server URL` field with the URL of your memoQ server.
6. Fill in the `User Name` field with your memoQ server user name.
7. Fill in the `Password` field with your memoQ server password.
8. Choose the `Login mode` you want to use: memoQ server user or windows user.
9. Click on the `Connect` button.
10. Confirm that the connection has appeared and the status is `Connected`

## Actions

### Termbase

- **Create term** Creates a new term entry in a termbase.
- **Update term**  Updates a specific term in a termbase.
- **Delete term** Deletes a specific term from a termbase.
- **Search termbases** Searches for termbases.

## Events

- **On termbase modified**  Triggers when a termbase is modified.

## Feedback

Do you want to use this app or do you have feedback on our implementation? Reach out to us using the [established channels](https://www.blackbird.io/) or create an issue.

<!-- end docs -->
