# Alpasec.Cli

![Logo](https://media.discordapp.net/attachments/1011767156951752868/1050272623688421468/image.png?width=350&height=176)

```
    ___    __            _____            _________ 
   /   |  / /___  ____ _/ ___/___  _____ / ____/ (_)
  / /| | / / __ \/ __ `/\__ \/ _ \/ ___// /   / / / 
 / ___ |/ / /_/ / /_/ /___/ /  __/ /___/ /___/ / /  
/_/  |_/_/ .___/\__,_//____/\___/\___(_)____/_/_/   
        /_/       
```

## What is it and what is it for

AlpaSec.Cli aims to protect files and their respective data stored locally by users, using methods for encryption and decryption.

## Installation

1. Run the install.sh file in project root.
   1. If this is the first installation remove the command `dotnet tool uninstall --global alpasec.cli` from the file, run the next step and put it back
2. Run the command `alpasec` in your terminal.
3. Create in %appdata%/Alpasec a folder named scripts.
4. Copy the encrypt.py from project and paste inside the scripts folder that was created earlier.  

Important: The encrypt.py use the version `38.0.3` of the `cryptography` library. *Working not guaranteed in other versions.*


## Commands

| Command {Required} [Optional] | Description | Permission Level |
| --- | --- | --- |
| alpasec --add-user {Username} --password {Password} | Add a new user | Admin |
| alpasec --delete-user {Username} | Delete a user | Admin |
| alpasec --change-user {Username} {[--new-name] [--new-password]} | Change user | Admin |
| alpasec --login {Username} --password {Password} | Login User | None |
| alpasec --logout | Logout User | None |
| alpasec [--no-move] --encrypt {FileName} | Encrypts a file | User |
| alpasec --decrypt {FileName} | Decrypts a file | User |
| alpasec --list-users | List all users | None |
| alpasec --list | List files in user folder | User |
| alpasec --help | Shows all arguments | None |
| alpasec --version | Shows Alpasec current version | None |

## ToDo

- [X] Write all commands in Readme
- [X] Implement command to list users
- [X] Implement command to change users
- [X] Implement command to delete users
- [X] Implement command list files inside user folder

# How to Contributing

Open pull request