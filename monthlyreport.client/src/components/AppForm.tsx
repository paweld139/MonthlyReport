import {
    Button,
    Col,
    Form,
    FormGroup,
    Row,
    RowProps
} from "reactstrap";

import {
    FormAction,
    FormInput
} from "../interfaces";

import {
    useState
} from "react";

import {
    getErrors
} from "../utils";

import AppInput from "./AppInput";

interface Props<T> {
    data: T;
    setData: (data: T) => void;
    inputs: FormInput<T>[];
    rowProps: RowProps;
    onSubmit: () => Promise<void | Response>;
    buttonLabel?: string;
    buttonColor?: string;
    actions?: FormAction[];
    idPrefix?: string;
}

const AppForm = <T,>({
    data,
    setData,
    inputs,
    rowProps,
    onSubmit,
    buttonLabel = 'Submit',
    buttonColor,
    actions,
    idPrefix
}: Props<T>) => {
    const [errors, setErrors] = useState<Record<string, string>>();

    return (
        <Form
            onSubmit={async (event) => {
                event.preventDefault();

                const response = await onSubmit();

                if (response) {
                    const errors = await getErrors(response);

                    if (errors) {
                        setErrors(errors);
                    }
                    else {
                        setErrors(undefined);
                    }
                }
            }}
        >
            <Row
                {...rowProps}
            >
                {inputs.map((input, index) => (
                    <FormGroup
                        key={index}
                    >
                        <AppInput
                            type={input.type}
                            idPrefix={idPrefix}
                            required={input.required}
                            options={input.options}
                            data={data}
                            setData={setData}
                            property={input.property}
                            label={input.label}
                            errors={errors}
                        />
                    </FormGroup>
                ))}
            </Row>


            <Row>
                <Col
                    xs="auto"
                >
                    <Button
                        color={buttonColor}
                    >
                        {buttonLabel}
                    </Button>
                </Col>

                {actions?.map((action, index) =>
                    <Col
                        key={index}
                        xs="auto"
                    >
                        <Button
                            onClick={action.onClick}
                            color={action.color}
                        >
                            {action.label}
                        </Button>
                    </Col>
                )}
            </Row>
        </Form>
    );
};

export default AppForm;