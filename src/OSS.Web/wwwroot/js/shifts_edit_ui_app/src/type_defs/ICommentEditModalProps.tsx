import { IShift } from "./IShift";

export interface ICommentEditModalProps {
    show: boolean;
    setShow: (x: boolean) => void;
    shift: IShift;
    comment: string;
    onCommentSubmit: (comm: string, s: IShift) => void;
}
