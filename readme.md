# Alpasec.Cli

## What is it and what is it for

AlpaSec.Cli aims to protect files and their respective data stored locally by users, using methods for encryption and decryption.

## Installation

1. Run the install.sh file in project root.
2. Run the command `alpasec` in your terminal.
3. Create in %appdata%/Alpasec a folder named scripts.
4. Copy the encrypt.py from project and paste inside the scripts folder that was created earlier.      

## Commands

| Command {Required} [Optional] | Description | Permission Level |
| --- | --- | --- |
| alpasec --add-user {Username} --password {Password} | Add a new user | Admin |
| alpasec --login {Username} --password {Password} | Login User | None |
| alpasec --logout | Logout User | None |
| alpasec --encrypt {FileName} | Encrypts a file | User |
| alpasec --decrypt {FileName} | Decrypts a file | User |
| alpasec --help | Shows all commands | None |
| alpasec --version | Shows Alpasec current version | None |

## ToDo

- [X] Write all commands in Readme
- [ ] Implement command to list users
- [ ] Implement command to change users
- [ ] Implement command list files inside user folder

# How to Contributing

Open pull request