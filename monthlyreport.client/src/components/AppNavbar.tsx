import {
    Navbar,
    NavbarBrand,
    NavbarText,
    Button
} from "reactstrap";

interface Props {
    title: string;
    text: string;
    buttonText: string;
    buttonOnClick: () => void;
}

const AppNavbar = ({
    title,
    text,
    buttonText,
    buttonOnClick
}: Props) => {
    return (
        <Navbar>
            <NavbarBrand>{title}</NavbarBrand>

            <NavbarText className="d-flex align-items-center">
                {text}

                <Button
                    color="link"
                    onClick={buttonOnClick}
                >
                    {buttonText}
                </Button>
            </NavbarText>
        </Navbar>
    );
};

export default AppNavbar;