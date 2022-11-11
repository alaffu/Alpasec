import base64
import os
import sys

from cryptography.fernet import Fernet
from cryptography.hazmat.primitives import hashes
from cryptography.hazmat.primitives.kdf.pbkdf2 import PBKDF2HMAC

class Encrypt:
    def encode_key(self, password):
        salt = b'e&\xa0\xdev\xbcT\x1b\x03H\xc6\xe9\x99\xc1\x9b0'

        kdf = PBKDF2HMAC(
            algorithm=hashes.SHA256(),
            length=32,
            iterations=390000,
            salt=salt
        )
        
        key = base64.urlsafe_b64encode(kdf.derive(password))
        fernet = Fernet(key)
        return fernet

    def is_file_encrypted(self, file_path):
        with open(file_path, 'rb') as file:
            content_file = file.read()

        if (content_file.startswith(b'gAAAAA')):
            return True
        else:
            return False

    def get_current_path(self):
        return os.getcwd()

    def encrypt_file(self, file_path, key):
        if (self.is_file_encrypted(file_path)):
            print('>> This file is already encrypted')
            sys.exit()

        text_file_to_encrypt = file_path

        fernet = self.encode_key(key)

        with open(text_file_to_encrypt, 'rb') as file:
            content_file = file.read()

        content_encrypted = fernet.encrypt(content_file)

        with open(text_file_to_encrypt, 'wb') as file_encrypted:
            file_encrypted.write(content_encrypted)


    def decrypt_file(self, file_path, key):
        if (not self.is_file_encrypted(file_path)):
            print('>> This file is not encrypted')
            sys.exit()

        fernet = self.encode_key(key)

        with open(file_path, 'rb') as file:
            content_file = file.read()

        try:
            content_decrypted = fernet.decrypt(content_file)
        except:
            print('>> The key passed is not correct')
            sys.exit()

        with open(file_path, 'wb') as file_encrypted:
            file_encrypted.write(content_decrypted)


def main():
    encrypt = Encrypt()

    currentPath = encrypt.get_current_path()
    file = os.path.join(currentPath, sys.argv[2])

    password = sys.argv[3].encode()

    if (os.path.exists(file)):
        if (os.path.isdir(file)):
            print('>> This is a directory')
            return

        if (sys.argv[1] == "encrypt"):
            encrypt.encrypt_file(file, password)
            print(f">> File {sys.argv[2]} encrypted")
        elif (sys.argv[1] == "decrypt"):
            encrypt.decrypt_file(file, password)
            print(f">> File {sys.argv[2]} decrypted")
    else:
        print(">> File not exists")


main()
