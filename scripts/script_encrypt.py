import base64
import os
import sys
import pathlib

from cryptography.fernet import Fernet
from cryptography.hazmat.primitives import hashes
from cryptography.hazmat.primitives.kdf.pbkdf2 import PBKDF2HMAC


def get_key_from_password(password):
    kdf = PBKDF2HMAC(
        algorithm=hashes.SHA256(),
        lenght=32,
        iteration=390000,
    )
    key = base64.urlsafe_b64encode(kdf.derive(password))
    return key


def creating_key(password=None, file_path=None):
    '''
        Pass password if you want to get the fernet key using a password
        or if you want to save a random fernet key to an file just pass the file_path to where
        it will be saved
    '''
    # if not password and file_path:
    #     raise "Requires password or a file path"

    if file_path:
        key = Fernet.generate_key()
        with open(file_path, "wb") as filekey:
            filekey.write(key)
        return key

    elif password:
        return get_key_from_password(password)


def accessing_key(file_path=None, password=None):
    if file_path:
        with open(file_path, "rb") as filekey:
            key = filekey.read()
        return key
    if password:
        return get_key_from_password(password)


def encrypt_file(file_path, filekey=None, password=None):
    text_file_to_encrypt = file_path
    FILE_KEY = filekey

    creating_key(file_path=FILE_KEY)
    key = accessing_key(file_path=FILE_KEY)
    fernet = Fernet(key)

    with open(text_file_to_encrypt, 'rb') as file:
        content_file = file.read()

    content_encrypted = fernet.encrypt(content_file)

    with open(text_file_to_encrypt, 'wb') as file_encrypted:
        file_encrypted.write(content_encrypted)


def decrypt_file(file_path, filekey):
    key = accessing_key(filekey)
    fernet = Fernet(key)

    with open(file_path, 'rb') as file:
        content_file = file.read()

    content_decrypted = fernet.decrypt(content_file)

    with open(file_path, 'wb') as file_encrypted:
        file_encrypted.write(content_decrypted)


def read_file(file_path):
    with open(file_path, "r") as file:
        print(file.read)


def get_size_file(file_path):
    return os.path.getsize(file_path)


def get_absolute_path():
    return pathlib.Path().resolve()


def get_parent_path(path):
    return os.path.abspath(os.path.join(path, os.pardir))


def main():
    ABSOLUTE_DIRECTORY_PATH = get_absolute_path()
    THIS_SCRIPT_PATH = str(ABSOLUTE_DIRECTORY_PATH) + '/scripts'
    FILE_KEY = THIS_SCRIPT_PATH + "filekey.key"

    # PARENT_DIRECTORY_PATH = get_parent_path(CURRENT_DIRECTORY_PATH)

    text_file_to_encrypt = str(ABSOLUTE_DIRECTORY_PATH) + '/test_files/simple_text_file.txt'

    password = "teste"

    # read_file(text_file_to_encrypt)
    print(get_size_file(text_file_to_encrypt))

    encrypt_file(text_file_to_encrypt, FILE_KEY)

    # read_file(text_file_to_encrypt)
    print(get_size_file(text_file_to_encrypt))

    with open(text_file_to_encrypt, "r") as file:
        print(os.path.getsize(text_file_to_encrypt))
        print(file.read())

    decrypt_file(text_file_to_encrypt, FILE_KEY)

    with open(text_file_to_encrypt, "r") as file:
        print(os.path.getsize(text_file_to_encrypt))
        print(file.read())


main()
