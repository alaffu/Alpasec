import base64
import os
import pathlib

from cryptography.fernet import Fernet
from cryptography.hazmat.primitives import hashes
from cryptography.hazmat.primitives.kdf.pbkdf2 import PBKDF2HMAC


def get_key_by_password(password):
    salt = os.urandom(16);

    kdf = PBKDF2HMAC(
        algorithm=hashes.SHA256(),
        length=32,
        iterations=390000,
        salt=salt
    )

    key = base64.urlsafe_b64encode(kdf.derive(password))
    return key

def create_key_by_file(file_path):
        key = Fernet.generate_key()
        with open(file_path, "wb") as filekey:
            filekey.write(key)
        return key


def get_key_from_file(file_path):
    with open(file_path, "rb") as filekey:
        key = filekey.read()
    return key


def encrypt_file(file_path, filekey=None):
    text_file_to_encrypt = file_path
    FILE_KEY = filekey

    create_key_by_file(file_path=FILE_KEY)
    key = get_key_from_file(file_path=FILE_KEY)
    fernet = Fernet(key)

    with open(text_file_to_encrypt, 'rb') as file:
        content_file = file.read()

    content_encrypted = fernet.encrypt(content_file)

    with open(text_file_to_encrypt, 'wb') as file_encrypted:
        file_encrypted.write(content_encrypted)


def decrypt_file(file_path, filekey):
    key = get_key_from_file(filekey)
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


def encrypt_file_password_method(file_path,  key):
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


def decrypt_file_password_method(file_path_decrypt, key):
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


def encrypt_file_key_file_method(text_file_to_encrypt, file_key):

    print(get_size_file(text_file_to_encrypt))

    encrypt_file(text_file_to_encrypt, file_key)

    print(get_size_file(text_file_to_encrypt))

    with open(text_file_to_encrypt, "r") as file:
        print(os.path.getsize(text_file_to_encrypt))
        print(file.read())

    decrypt_file(text_file_to_encrypt, file_key)

    with open(text_file_to_encrypt, "r") as file:
        print(os.path.getsize(text_file_to_encrypt))
        print(file.read())


def file_method_handler(text_file_to_encrypt, file_key):
    print(get_size_file(text_file_to_encrypt))

    encrypt_file(text_file_to_encrypt, file_key)
    with open(text_file_to_encrypt, "r") as file:
        print(os.path.getsize(text_file_to_encrypt))
        print(file.read())

    decrypt_file(text_file_to_encrypt, file_key)

    with open(text_file_to_encrypt, "r") as file:
        print(os.path.getsize(text_file_to_encrypt))
        print(file.read())


def password_method_handler(text_file_to_encrypt, password):
    key = get_key_by_password(password)

    encrypt_file_password_method(text_file_to_encrypt, key)

    decrypt_file_password_method(text_file_to_encrypt, key)


def main():
    ABSOLUTE_DIRECTORY_PATH = get_absolute_path()
    THIS_SCRIPT_PATH = str(ABSOLUTE_DIRECTORY_PATH) + '/scripts'
    FILE_KEY = THIS_SCRIPT_PATH + "/filekey.key"

    TEXT_FILE_TO_ENCRYPT = str(ABSOLUTE_DIRECTORY_PATH) + '/test_files/simple_text_file.txt'

    password = b"teste"

    if password:
        password_method_handler(TEXT_FILE_TO_ENCRYPT, password);
    else:
        file_method_handler(TEXT_FILE_TO_ENCRYPT, FILE_KEY)

main()
