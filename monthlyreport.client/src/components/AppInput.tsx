import moment from "moment";

import {
    Input,
    Label
} from "reactstrap";

import {
    InputType
} from "reactstrap/types/lib/Input";

import AppInputError from "./AppInputError";

import {
    useMemo
} from "react";

interface Props<T> {
    type?: InputType;
    idPrefix?: string;
    required?: boolean;
    options?: string[];
    label?: string;
    data: T;
    setData: (data: T) => void;
    property: keyof T;
    errors?: Record<string, string>;
}

const AppInput = <T,>({
    type,
    idPrefix,
    required,
    options,
    label,
    data,
    setData,
    property,
    errors
}: Props<T>) => {
    const id = useMemo(() => label && `${idPrefix}${idPrefix && '-'}${property.toString()}`, [idPrefix, label, property]);

    const value = useMemo(() => {
        switch (type) {
            case 'datetime-local':
                return moment(data[property] as Date).format('yyyy-MM-DDTHH:mm');
            default:
                return String(data[property]);
        }
    }, [data, property, type]);

    const onChange = useMemo(() => (event: React.ChangeEvent<HTMLInputElement>) => {
        let value;

        switch (type) {
            case 'datetime-local':
                value = moment(event.target.value).toDate();
                break;
            case 'number':
                value = Number(event.target.value);
                break;
            default:
                value = event.target.value;
                break;
        };

        setData({
            ...data,
            [property]: value
        });
    }, [data, setData, property, type]);

    return (
        <>
            {label &&
                <Label
                    for={id}
                >
                    {label}
                </Label>}

            <Input
                type={type}
                name={label && property.toString()}
                id={id}
                required={required}
                value={value}
                onChange={onChange}
            >
                {options?.map((option, index) => (
                    <option
                        key={index}
                    >
                        {option}
                    </option>
                ))}
            </Input>

            {errors &&
                <AppInputError
                    errors={errors}
                    property={property.toString()}
                />}
        </>
    );
};

export default AppInput;