/** @jsxImportSource @emotion/react */
import { pageContainer } from "../../Styles/General/PageStyles";
import { PageTitle } from "./PageTitle";

interface Props {
  title?: string;
  children: React.ReactNode;
}

export const Page = ({ title, children }: Props) => (
  <div css={pageContainer}>
    {title && <PageTitle>{title}</PageTitle>}
    {children}
  </div>
);
