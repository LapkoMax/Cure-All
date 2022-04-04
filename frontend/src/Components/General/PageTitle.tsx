/** @jsxImportSource @emotion/react */

import { pageTitleContainer } from "../../Styles/General/PageTitleStyles";

interface Props {
  children: React.ReactNode;
}
export const PageTitle = ({ children }: Props) => (
  <h2 css={pageTitleContainer}>{children}</h2>
);
