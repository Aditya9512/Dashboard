import { useState, useEffect } from 'react';

function Message({ variant, children }) {
	const [show, setShow] = useState(true);

	// On componentDidMount set the timer
	useEffect(() => {
		const timeId = setTimeout(() => {
			// After 3 seconds set the show value to false
			setShow(false);
		}, 10000);

		return () => {
			clearTimeout(timeId);
		};
	}, []);

	// If show is false the component will return null and stop here
	if (!show) {
		return null;
	}

	// If show is true this will be returned
	return (
		<div className={`alert alert-${variant}`}>
			{children}
		</div>
	);
}

Message.defaultPros = {
	variant: 'info',
}

export default Message;
