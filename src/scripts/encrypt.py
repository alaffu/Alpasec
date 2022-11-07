import base64
import os
import pathlib
import sys

from cryptography.fernet import Fernet
from cryptography.hazmat.primitives import hashes
from cryptography.hazmat.primitives.kdf.pbkdf2 import PBKDF2HMAC

class Encrypt:
    def get_key_by_password(self, password):
        salt = os.urandom(16);

        kdf = PBKDF2HMAC(
            algorithm=hashes.SHA256(),
            length=32,
            iterations=390000,
            salt=salt
        )

        key = base64.urlsafe_b64encode(kdf.derive(password))
        return key

    def create_key_by_file(self, file_path):
        key = Fernet.generate_key()
        with open(file_path, "wb") as filekey:
            filekey.write(key)
        return key


    def get_key_from_file(self, file_path):
        with open(file_path, "rb") as filekey:
            key = filekey.read()
        return key


    def encrypt_file(self, file_path, filekey=None):
        text_file_to_encrypt = file_path
        FILE_KEY = filekey

        self.create_key_by_file(file_path=FILE_KEY)
        key = self.get_key_from_file(file_path=FILE_KEY)
        fernet = Fernet(key)

        with open(text_file_to_encrypt, 'rb') as file:
            content_file = file.read()

        content_encrypted = fernet.encrypt(content_file)

        with open(text_file_to_encrypt, 'wb') as file_encrypted:
            file_encrypted.write(content_encrypted)


    def decrypt_file(self, file_path, filekey):
        key = self.get_key_from_file(filekey)
        fernet = Fernet(key)

        with open(file_path, 'rb') as file:
            content_file = file.read()

        content_decrypted = fernet.decrypt(content_file)

        with open(file_path, 'wb') as file_encrypted:
            file_encrypted.write(content_decrypted)


    def read_file(self, file_path):
        with open(file_path, "r") as file:
            print(file.read)


    def get_size_file(self, file_path):
        return os.path.getsize(file_path)

    def get_current_path(self):
        return os.getcwd()

    def get_absolute_path(self):
        return pathlib.Path().resolve()


    def get_parent_path(self, path):
        return os.path.abspath(os.path.join(path, os.pardir))


    def encrypt_file_password_method(self, file_path,  key):
        FILE_TO_ENCRYPT = file_path
        # KEY = get_key_by_password(password)
        KEY = key

        fernet = Fernet(KEY)
        print(FILE_TO_ENCRYPT)

        with open(FILE_TO_ENCRYPT, 'rb') as file:
            content_file = file.read()

        encrypted_content = fernet.encrypt(content_file)

        with open(FILE_TO_ENCRYPT, 'wb') as file_encrypted:
            file_encrypted.write(encrypted_content)


    def decrypt_file_password_method(self, file_path_decrypt, key):
        FILE_TO_DECRYPT = file_path_decrypt
        # KEY = get_key_by_password(password)
        KEY = key

        fernet = Fernet(KEY)

        with open(FILE_TO_DECRYPT, 'rb') as file:
            content_file = file.read()
            print(content_file)

        decrypted_content = fernet.decrypt(content_file)
        
        with open(FILE_TO_DECRYPT, 'wb') as file_decrypted:
            file_decrypted.write(decrypted_content)

        with open(FILE_TO_DECRYPT, 'rb') as file:
            content_file = file.read()
            print(str(content_file))


    def encrypt_file_key_file_method(self, text_file_to_encrypt, file_key):

        print(self.get_size_file(text_file_to_encrypt))

        self.encrypt_file(text_file_to_encrypt, file_key)

        print(self.get_size_file(text_file_to_encrypt))

        with open(text_file_to_encrypt, "r") as file:
            print(os.path.getsize(text_file_to_encrypt))
            print(file.read())

        self.decrypt_file(text_file_to_encrypt, file_key)

        with open(text_file_to_encrypt, "r") as file:
            print(os.path.getsize(text_file_to_encrypt))
            print(file.read())


    def file_method_handler(self, text_file_to_encrypt, file_key):
        print(self.get_size_file(text_file_to_encrypt))

        self.encrypt_file(text_file_to_encrypt, file_key)
        with open(text_file_to_encrypt, "r") as file:
            print(os.path.getsize(text_file_to_encrypt))
            print(file.read())

        self.decrypt_file(text_file_to_encrypt, file_key)

        with open(text_file_to_encrypt, "r") as file:
            print(os.path.getsize(text_file_to_encrypt))
            print(file.read())

    def password_method_handler(self, text_file_to_encrypt, password):
        key = self.get_key_by_password(password)

        self.encrypt_file_password_method(text_file_to_encrypt, key)

        self.decrypt_file_password_method(text_file_to_encrypt, key)


def main():
    # Create directory named Alpasec in %Appdata%
    # Create directory named scripts in %Appdata%\Alpasec
    # Drop this script in %Appdata%\Alpasec\scripts

    encrypt = Encrypt()
    currentPath = encrypt.get_current_path()
    file = os.path.join(currentPath, sys.argv[2])

    password = b"vGSQKxjrXWGR6tHn"

    if (os.path.exists(file)):
        if (sys.argv[1] == "encrypt"):
            encrypt.encrypt_file(file, password)
        elif (sys.argv[1] == "decrypt"):
            encrypt.decrypt_file(file, password)
    else:
        print(">> File not exists!")

main()
