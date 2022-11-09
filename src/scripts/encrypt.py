import os
import sys

from cryptography.fernet import Fernet

class Encrypt:
    def get_current_path(self):
        return os.getcwd()

    def encrypt_file(self, file_path, key):
        text_file_to_encrypt = file_path

        fernet = Fernet(key)

        with open(text_file_to_encrypt, 'rb') as file:
            content_file = file.read()

        content_encrypted = fernet.encrypt(content_file)

        with open(text_file_to_encrypt, 'wb') as file_encrypted:
            file_encrypted.write(content_encrypted)


    def decrypt_file(self, file_path, key):
        fernet = Fernet(key)

        with open(file_path, 'rb') as file:
            content_file = file.read()

        content_decrypted = fernet.decrypt(content_file)

        with open(file_path, 'wb') as file_encrypted:
            file_encrypted.write(content_decrypted)


def main():
    encrypt = Encrypt()

    currentPath = encrypt.get_current_path()
    file = os.path.join(currentPath, sys.argv[2])

    password = sys.argv[3]

    if (os.path.exists(file)):
        if (sys.argv[1] == "encrypt"):
            encrypt.encrypt_file(file, password)
        elif (sys.argv[1] == "decrypt"):
            encrypt.decrypt_file(file, password)
    else:
        print(">> File not exists!")


main()
