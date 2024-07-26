# Publish Changes to Unity

Publishing library changes for Unity developers to sync is completed not when code is committed, but when **tags** are committed to the repository.


### When to Publish

When the `main` branch contains a new iteration of the library that you'd like to publish for unity devs...


### How to Publish
- Create a git tag for the version
```bash
git tag -a v1.0.0 -m "message if you want one"
```
- Push the tag to the remote
```bash
git push origin v1.0.0
```

- GitHub actions will create a release for `TicTacToe` each time a version tag is created