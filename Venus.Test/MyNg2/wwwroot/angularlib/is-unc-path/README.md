# is-unc-path [![NPM version](https://badge.fury.io/js/is-unc-path.svg)](http://badge.fury.io/js/is-unc-path)

> Returns true if a filepath is a windows UNC file path.

Install with [npm](https://www.npmjs.com/)

```sh
$ npm i is-unc-path --save
```

## Usage

```js
var isUncPath = require('is-unc-path');
```

**true**

Returns true for windows UNC paths:

```js
isUncPath('\\/foo/bar');
isUncPath('\\\\foo/bar');
isUncPath('\\\\foo\\admin$');
isUncPath('\\\\foo\\admin$\\system32');
isUncPath('\\\\foo\\temp');
isUncPath('\\\\/foo/bar');
isUncPath('\\\\\\/foo/bar');
```

**false**

Returns false for non-UNC paths:

```js
isUncPath('/foo/bar');
isUncPath('/');
isUncPath('/foo');
isUncPath('/foo/');
isUncPath('c:');
isUncPath('c:.');
isUncPath('c:./');
isUncPath('c:./file');
isUncPath('c:/');
isUncPath('c:/file');
```

**Customization**

Use `.source` to use the regex as a component of another regex:

```js
var myRegex = new RegExp(isUncPath.source + 'foo');
```

**[Rules for UNC paths](http://resources.esri.com/help/9.3/ArcGISDesktop/com/Gp_ToolRef/sharing_tools_and_toolboxes/pathnames_explained_colon_absolute_relative_unc_and_url.htm)**

* The computer name is always preceded by a double backward-slash (`\\`).
* UNC paths cannot contain a drive letter (such as `D:`)

## Related projects

* [dotfile-regex](https://github.com/regexps/dotfile-regex): Regular expresson for matching dotfiles.
* [dotdir-regex](https://github.com/regexps/dotdir-regex): Regex for matching dot-directories, like `.git/`
* [dirname-regex](https://github.com/regexps/dirname-regex): Regular expression for matching the directory part of a file path.
* [is-glob](https://github.com/jonschlinkert/is-glob): Returns `true` if the given string looks like a glob pattern.
* [path-regex](https://github.com/regexps/path-regex): Regular expression for matching the parts of a file path.
* [unc-path-regex](https://github.com/jonschlinkert/unc-path-regex): Returns true if a filepath is a windows UNC file path.

## Running tests

Install dev dependencies:

```sh
$ npm i -d && npm test
```

## Contributing

Pull requests and stars are always welcome. For bugs and feature requests, [please create an issue](https://github.com/jonschlinkert/is-unc-path/issues/new)

## Author

**Jon Schlinkert**

+ [github/jonschlinkert](https://github.com/jonschlinkert)
+ [twitter/jonschlinkert](http://twitter.com/jonschlinkert)

## License

Copyright © 2015 Jon Schlinkert
Released under the MIT license.

***

_This file was generated by [verb-cli](https://github.com/assemble/verb-cli) on July 07, 2015._