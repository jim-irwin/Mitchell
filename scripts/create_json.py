import json


def create_json_file(data, file_path):
    with open(file_path, 'w') as fh:
        json.dump(data, fh, indent=4, sort_keys=True)


def get_json_file_data(file_path):
    with open(file_path, 'r') as fh:
        return json.load(fh)


if __name__ == '__main__':

    fp = 'test.json'

    a = {'a':[{'foo':1},2,3], 'b':2, 'c':3}

    create_json_file(a, fp)

    a = get_json_file_data(fp)

    a['b'] = 10

    create_json_file(a, fp)

    pass




