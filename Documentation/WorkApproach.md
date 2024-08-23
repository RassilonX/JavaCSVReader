# Work Approach

Due to this being an application using multiple languages with an active API, the happy path was developed first and checked with manual testing. This was done to make sure that any errors in further testing were not due to the program being configured incorrectly, but by bad data being manipulated.

The C# side has a mix of Manual, Integration, BDD, and TDD as an approach. Manual was used to make sure everything was conected properly, Integration to make sure the repository was functioning properly, BDD to check the Happy Path is being tested for correctly, and TDD for the unhappy path routes and corrections to make the program more resilient to it.

The Java side was developed with manual testing first, then a TDD approach. This is because of things such as the SSL handshake issue that cropped up during development, needing to be ironed out to ensure failing tests were due to bad data and not due to incorrect configurations.