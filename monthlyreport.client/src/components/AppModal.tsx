import {
    ReactNode,
    useCallback
} from "react";

import {
    Button,
    Modal,
    ModalBody,
    ModalFooter,
    ModalHeader
} from "reactstrap";

interface Props {
    header: string;
    isOpen: boolean;
    setIsOpen: (isOpen: boolean) => void;
    children: ReactNode;
    onAccept?: () => void;
}

const AppModal = ({
    header,
    children,
    isOpen,
    setIsOpen,
    onAccept
}: Props) => {
    const toggle = useCallback(() => setIsOpen(!isOpen), [isOpen, setIsOpen]);

    return (
        <Modal
            isOpen={isOpen}
            toggle={toggle}
        >
            <ModalHeader
                toggle={toggle}
            >
                {header}
            </ModalHeader>

            <ModalBody>
                {children}
            </ModalBody>

            {onAccept &&
                <ModalFooter>
                    <Button
                        color="danger"
                        onClick={toggle}
                    >
                        No
                    </Button>

                    <Button
                        color="primary"
                        onClick={onAccept}
                    >
                        Yes
                    </Button>
                </ModalFooter>}
        </Modal >
    );
}

export default AppModal;