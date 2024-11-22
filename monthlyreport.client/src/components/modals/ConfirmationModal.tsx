import {
    ConfirmationModalSettings
} from "../../interfaces";

import AppModal from "../AppModal";

interface Props {
    settings: ConfirmationModalSettings;
    setIsOpen: (isOpen: boolean) => void;
    message: string;
}

const ConfirmationModal = ({
    settings,
    setIsOpen,
    message
}: Props) => {
    return (
        <AppModal
            header="Confirmation"
            isOpen={settings.isOpen}
            setIsOpen={setIsOpen}
            onAccept={settings.onAccept}
        >
            <p>{message}</p>
        </AppModal>
    );
};

export default ConfirmationModal;