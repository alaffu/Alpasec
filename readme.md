# Alpasec.Cli

## What is it and what is it for

AlpaSec.Cli aims to protect files and data stored locally in users' folders, using encryption and decryption methods.

## Installation

                
1. Create in %appdata% a folder named Alpasec. inside another folder with the name scripts.
2. Copy the encrypt.py from project and paste inside the scripts folder that was created earlier.
3. In install.sh comment the command line with dotnet tool uninstall (if the cli is not already installed).
4. Run in root of project this command >> ./install.sh <<.
                

## Commands

| Command | Description |
| --- | --- |
| alpasec --encrypt {FileName} | Encrypts a file |
| alpasec --decrypt {FileName} | Decrypts a file |
| alpasec --help | Shows all commands |
| alpasec --version | Shows Alpasec current version |

## ToDo

- [ ] Write all commands in Readme
- [ ] Implement command to list users
- [ ] Implement command to change users
- [ ] Implement command list files inside user folder

# How to Contributing

Open pull request