import { UserIcon } from "./Icons";

export const Header = () => {
  return (
    <div>
      <a href="./">Cure-All</a>
      <input type="text" placeholder="Search for doctor..." />
      <a href="./signin">
        <UserIcon />
        <span>Sign In</span>
      </a>
    </div>
  );
};
