# BasicCompiler

A C# reimplementation of the code at https://github.com/thejameskyle/the-super-tiny-compiler.

## What does it do?

It takes Scheme-like syntax and converts it to C-style function calls:

```
$ cat input.txt
(add (subtract 4 2) 4)

$ BasicCompiler input.txt
add(subtract(4, 2), 4)
```

## Project structure

- Compiler logic lives in `src/BasicCompiler.Core`
- Tests are in `src/BasicCompiler.Tests`
- A sample program that compiles a file and prints the results is in `src/BasicCompiler`

## Notes

- This project uses C# 7; you need VS 2017 or later to compile it.

## TODO

- Negative numbers

## License

[MIT](LICENSE)
