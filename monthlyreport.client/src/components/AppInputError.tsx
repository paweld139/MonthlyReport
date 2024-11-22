interface Props {
    errors: Record<string, string>,
    property: string
}

const AppInputError = ({
    errors,
    property
}: Props) => {
    return (
        errors[property] && (
            <div
                className="text-danger"
            >
                {errors[property]}
            </div>
        )
    );
};

export default AppInputError;