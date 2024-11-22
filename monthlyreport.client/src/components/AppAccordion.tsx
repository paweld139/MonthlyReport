import {
    useCallback,
    useState
} from "react";

import {
    Accordion,
    AccordionBody,
    AccordionHeader,
    AccordionItem
} from "reactstrap";

import {
    AppAccordionItem
} from "../interfaces";

interface Props {
    className?: string;
    items: AppAccordionItem[];
    defaultOpen?: string;
}

const AppAccordion = ({
    className,
    items,
    defaultOpen = ''
}: Props) => {
    const [open, setOpen] = useState(defaultOpen);

    const toggle = useCallback((id: string) => {
        if (open === id) {
            setOpen('');
        }
        else {
            setOpen(id);
        }
    }, [open]);

    return (
        <Accordion
            toggle={toggle}
            open={open}
            className={className}
        >
            {items.map((item, index) => (
                <AccordionItem
                    key={index}
                    id={index.toString()}
                >
                    <AccordionHeader
                        targetId={index.toString()}
                    >
                        {item.header}
                    </AccordionHeader>

                    <AccordionBody
                        accordionId={index.toString()}
                    >
                        {item.body}
                    </AccordionBody>
                </AccordionItem>
            ))}
        </Accordion>
    );
};

export default AppAccordion;